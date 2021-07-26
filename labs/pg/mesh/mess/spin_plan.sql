create or replace view mesh.spin_plan
as select
    url,
    null::text as spin,
    content,
    --TODO:The rest of field are output for debugging purposes
    '--' as remove,
    id,
    visited,
    spin as spin_id
from
    mess.spin
where
    profile = current_user
    and spin is null
;
