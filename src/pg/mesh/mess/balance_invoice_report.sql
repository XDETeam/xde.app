CREATE OR REPLACE FUNCTION mesh.balance_invoice_report(_url text)
RETURNS TABLE (
    chapter text,
    day date,
    weekday text,
    lot numeric,
    lot_week numeric,
    lot_month numeric,
    value numeric,
    value_week numeric,
    value_month numeric,
    unit text,
    tasks text
)
language sql
as $$
select
    chapter,
    day,
    to_char(day, 'Day') as weekday,
    lot,
    sum(lot) over (
        partition by chapter, to_char(day, 'IYYY-MM-IW'::text)
        order by day rows between unbounded preceding and current row
    ) AS lot_week,
    sum(lot) over (
        partition by chapter, to_char(day, 'IYYY-MM'::text)
        order by day rows between unbounded preceding and current row
    ) AS lot_month,
    value,
    sum(value) over (
        partition by chapter, to_char(day, 'IYYY-MM-IW'::text)
        order by day rows between unbounded preceding and current row
    ) AS value_week,
    sum(value) over (
        partition by chapter, to_char(day, 'IYYY-MM'::text)
        order by day rows between unbounded preceding and current row
    ) AS value_month,
    unit,
    tasks
from
    mesh.balance_invoice(_url) as balance
order by
    chapter asc,
    day desc
;
$$;
