CREATE OR REPLACE FUNCTION mesh.balance_invoice(_url text)
RETURNS TABLE (
    chapter text,
    day date,
    tasks text,
    unit text,
    cost numeric,
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
    cost,
    sum(lot),
    sum(value)
FROM
     mesh.balance_list
WHERE
    "to" ~ (_url || '.*')::lquery
    AND value > 0
GROUP BY
    overlay("to"::text placing '' from 1 for length(_url) + 1),
    "on",
    unit,
    cost
ORDER BY
    chapter,
    "on"
;
$$;
