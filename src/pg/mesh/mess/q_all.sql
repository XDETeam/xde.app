create or replace view mesh.q_all
as
select
    *
from
    mesh.q_scan(null)
;
