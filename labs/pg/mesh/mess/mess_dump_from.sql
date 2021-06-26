CREATE OR REPLACE PROCEDURE mesh.mess_dump_from(
    _filename text
)
AS $$
BEGIN
    TRUNCATE mess.mess;

    EXECUTE format(
        'COPY mess.mess FROM %L CSV HEADER',
        _filename
    );
END $$ LANGUAGE plpgsql;
