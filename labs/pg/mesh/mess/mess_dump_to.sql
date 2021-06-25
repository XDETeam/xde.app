create procedure mess_dump_to(_filename text)
    language plpgsql
as
$$
BEGIN
    EXECUTE format(
        'COPY (SELECT * FROM mesh.mess ORDER BY id) TO %L CSV HEADER',
        _filename
    );
END
$$;

alter procedure mess_dump_to(text) owner to postgres;

