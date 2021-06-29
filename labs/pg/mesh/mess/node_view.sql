CREATE OR REPLACE VIEW mesh.node_view AS
SELECT
    node.*,
    log.notes
FROM
    mess.node
    LEFT OUTER JOIN mess.log ON (
        log.id = node.id
        AND log.version = node.version
    )
;
