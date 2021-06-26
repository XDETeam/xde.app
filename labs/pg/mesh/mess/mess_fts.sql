CREATE OR REPLACE FUNCTION mesh.mess_fts(_query text)
RETURNS SETOF mess.mess
AS $$
    SELECT
        *
    FROM
        mess.mess
    WHERE
        mess.content::text %> _query
    ;
$$ LANGUAGE sql;
