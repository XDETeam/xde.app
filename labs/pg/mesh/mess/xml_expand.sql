create or replace function mesh.xml_expand(_xml xml)
returns table (
    path text,
    dom text,
    content xml,
    level int
)
language sql
as $$
with recursive cte as (
    select
        ''::ltree as path,
        (xpath('name(/*)', _xml))[1] as dom,
        _xml as content,
        0 as level

    union all select
        parent.path || xml.index::text,
        (xpath('name(/*)', xml.content))[1] as dom,
        xml.content,
        parent.level + 1
    from
        cte as parent,
        lateral xmltable(
            '/*/*' passing parent.content columns
            index for ordinality,
            url text path('.'),
            content xml path('.')
        ) xml
)
select
    *
from
    cte
$$;
