CREATE OR REPLACE PROCEDURE mesh.mess_dump_from(
    _filename text
)
AS $$
BEGIN
    TRUNCATE mesh.mess;

    EXECUTE format(
        'COPY mesh.mess FROM %L CSV HEADER',
        _filename
    );
END $$ LANGUAGE plpgsql;
