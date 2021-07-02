CREATE OR REPLACE PROCEDURE mesh.node_dump_from(
    _filename text
)
AS $$
BEGIN
    TRUNCATE mess.node;

    EXECUTE format(
        'COPY mess.node FROM %L CSV HEADER',
        _filename
    );
END $$ LANGUAGE plpgsql;
