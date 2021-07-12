create or replace function mesh.q_scan(_url lquery)
returns table (
    id bigint,
    node ltree,
    url ltree,
    title text,
    win int,
    level int,
    content xml
)
language sql
as $$
with recursive flatten as (
    select
        id,
        url,
        content,
        0 as level
    from
        mesh.node_view
    where
        (_url is not null and url ~ _url)
        or (_url is null and nlevel(url) = 1)

    union all select
        child.id,
        child.url,
        child.content,
        parent.level + 1 as level
    from
        flatten as parent
        join mesh.node_view as child on (
            child.url <@ parent.url
            and nlevel(child.url) = nlevel(parent.url) + 1
        )
)
,quests as (
    select
        parent.id,
        parent.url as node,
        parent.url || child.path as url,
        (xpath('/*/@for', child.content))[1]::text as title,
        (xpath('/*/@win', child.content))[1]::text::int as win,
        parent.level + child.level as level,
        child.content as content
    from
         flatten as parent,
         lateral mesh.xml_expand(parent.content) as child
    where
      child.dom = 'q'
)
select
    *
from
    quests
where
    not exists(
        select
           *
        from
            quests as chidren
        where
            chidren.url <@ quests.url
            and chidren.url != quests.url
    )
$$;
