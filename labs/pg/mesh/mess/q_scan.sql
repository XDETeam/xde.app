create or replace function mesh.q_scan(
    _url lquery = null,
    _date timestamp = current_timestamp
)
returns table (
    id bigint,
    node ltree,
    url ltree,
    title text,
    win int,
    deadline int,
    term text,
    leaf boolean,
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
        or (
            _url is null
            and not exists(
                select
                    *
                from
                    mesh.node_view as parents
                where
                    parents.url @> node_view.url
                    and parents.url != node_view.url
            )
        )

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
        (xpath('/*/@start', child.content))[1]::text::timestamp as start,
        (xpath('/*/@end', child.content))[1]::text::timestamp as end,
        (xpath('/*/@term', child.content))[1]::text as term,
        parent.level + child.level as level,
        child.content as content
    from
         flatten as parent,
         lateral mesh.xml_expand(parent.content) as child
    where
      child.dom = 'q'
)
select
    id,
    node,
    url,
    title,
    win,
    case
        when "end" is null then case
            when start is not null then 666
            else null
        end
        else case
            when start is null then case
                when "end" < _date then 100
                else null
            end
            else case
                when start > "end" then 667
                when _date not between start and "end" then null
                when _date > "end" then 100
                else (
                    EXTRACT(epoch FROM _date - start)
                    / EXTRACT(epoch FROM "end" - start)
                    * 100
                )::int
            end
        end
    end as deadline,
    term,
    not exists(
        select
           *
        from
            quests as chidren
        where
            chidren.url <@ quests.url
            and chidren.url != quests.url
    ) as leaf,
    level,
    content
from
    quests
$$;
