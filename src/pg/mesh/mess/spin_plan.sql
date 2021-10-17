create or replace view mesh.spin_plan
as select
    spin.url,
    null::text as spin,
    (
        select
            last.content
        from
            mess.spin as last
        where
            last.url = spin.url
            and last.id != spin.id
            and last.content is not null
        order by
            spin.visited desc
        limit
            1
    ) as content,
    --TODO:The rest of field are output for debugging purposes
    '--' as remove,
    spin.id,
    spin.visited,
    spin.spin as spin_id
from
    mess.spin
where
    spin.profile = current_user
    and spin.spin is null
;
