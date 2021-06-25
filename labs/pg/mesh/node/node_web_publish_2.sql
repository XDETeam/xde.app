create view node_web_publish(path, content) as
WITH cte AS (
    SELECT node.path,
           mesh.node_xslt_transform(node.path, 'screen'::text) AS content
    FROM mesh.node
)
SELECT cte.path,
       cte.content
FROM cte
WHERE cte.content IS NOT NULL;

alter table node_web_publish
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on node_web_publish to yavulan;

