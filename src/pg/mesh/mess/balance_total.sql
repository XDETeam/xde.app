CREATE OR REPLACE FUNCTION mesh.balance_total(_url lquery)
RETURNS numeric
LANGUAGE sql
AS $$
    SELECT
        SUM(value)
    FROM
         mesh.balance_list
    WHERE
        "to" ~ _url
    ;
$$;
