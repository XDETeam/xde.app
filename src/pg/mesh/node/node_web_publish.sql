CREATE OR REPLACE VIEW mesh.node_web_publish
AS
WITH CTE AS (
    SELECT
           path,
           mesh.node_xslt_transform(node.path, 'screen') AS content
    FROM
         mesh.node
)
SELECT
    path,
    content
FROM
    CTE
WHERE
    content IS NOT NULL
;
