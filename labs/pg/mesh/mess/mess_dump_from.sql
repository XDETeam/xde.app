create procedure mess_dump_from(_filename text)
    language plpgsql
as
$$
BEGIN
    TRUNCATE mesh.mess;

    EXECUTE format(
        'COPY mesh.mess FROM %L CSV HEADER',
        _filename
    );
END
$$;

alter procedure mess_dump_from(text) owner to postgres;

