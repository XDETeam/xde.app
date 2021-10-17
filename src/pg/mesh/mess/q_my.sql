create or replace view mesh.q_my
as
select
    *
from
    mesh.q_scan(current_user::lquery)
;
