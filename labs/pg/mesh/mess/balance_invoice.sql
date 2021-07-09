CREATE OR REPLACE FUNCTION mesh.balance_invoice(_url text)
RETURNS TABLE (
    chapter text,
    day date,
    tasks text,
    unit text,
    lot numeric,
    value numeric
)
LANGUAGE sql
AS $$
SELECT
    overlay("to"::text placing '' from 1 for length(_url) + 1) as chapter,
    "on",
    string_agg(title || '.', ' '),
    unit,
    sum(lot),
    sum(value)
from
     mesh.balance_list
WHERE
    "to" ~ (_url || '.*')::lquery
    AND value > 0
GROUP BY
    overlay("to"::text placing '' from 1 for length(_url) + 1),
    "on",
    unit
ORDER BY
    chapter,
    "on"
;
$$;
