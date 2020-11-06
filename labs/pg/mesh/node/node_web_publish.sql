CREATE OR REPLACE FUNCTION mesh.node_web_publish()
RETURNS TABLE (
    path text,
    content text
)
AS $$
    WITH CTE AS (
        SELECT
               path,
               mesh.node_xslt_transform(node.path, 'screen') AS output
        FROM
             mesh.node
    )
    SELECT
        path,
        output
    FROM
        CTE
    WHERE
        output IS NOT NULL
    ;
$$ LANGUAGE SQL;
