CREATE OR REPLACE PROCEDURE mesh.mess_dump_to(
    _filename text
)
AS $$
BEGIN
    EXECUTE format(
        'COPY (SELECT * FROM mess.node ORDER BY id) TO %L CSV HEADER',
        _filename
    );
END $$ LANGUAGE plpgsql;
