create or replace view mesh.link_view
as select
    case type.name
        when 'parent' then child.url
        else parent.url
    end as source,
    case type.name
        when 'parent' then parent.url
        else child.url
    end as target,
    type.name as type
from
    mesh.node_view as parent
    join mesh.node_view as child on (
        child.url ~ (parent.url::text || '.*{1}')::lquery --(parent.url || '.*')
    )
    cross join (values ('parent'), ('child')) AS type(name)
where
    parent.url = 'stan'
;
