create or replace function mesh.balance_invoice_format(_url text)
returns table (
    chapter text,
    date text,
    tasks text,
    hours numeric(6,2),
    unit text,
    price text,
    value text
)
language sql
as $$
select
    chapter,
    to_char(day, 'dd.MM.yyyy'::text) AS date,
 	tasks,
    lot::numeric(6,2) as hours,
 	'hour/час'::text AS unit,
    '€' || cost::numeric(6,2)::text as price,
    '€' || value::numeric(6,2)::text as value
from
    mesh.balance_invoice(_url)
;
$$
