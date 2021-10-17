CREATE OR REPLACE FUNCTION mesh.node_fts(_query text)
RETURNS SETOF mesh.node_view
AS $$
    SELECT
        *
    FROM
        mesh.node_view
    WHERE
        node_view.content::text %> _query
    ;
$$ LANGUAGE sql;

COMMENT ON FUNCTION mesh.node_fts(_query text)
    IS 'Full-text search by nodes content'
;
