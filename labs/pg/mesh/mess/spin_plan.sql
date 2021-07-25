create or replace view mesh.spin_plan
as select
    current.url,
    current.content,
    --TODO:The rest of field are output for debugging purposes
    '--' as remove,
    current.id,
    current.chain,
    current.visited
from
    mess.spin as current
    left outer join mess.spin as next on (next.chain = current.id)
where
    current.author = current_user
    and next.id is null
;
