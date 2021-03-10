CREATE OR REPLACE FUNCTION mesh.mess_fts(_query text)
RETURNS SETOF mesh.mess
AS $$
    SELECT
        *
    FROM
        mesh.mess
    WHERE
        mess.content::text %> _query
    ;
$$ LANGUAGE sql;
