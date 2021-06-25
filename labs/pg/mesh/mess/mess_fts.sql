create function mess_fts(_query text) returns SETOF mesh.mess
    language sql
as
$$
SELECT
        *
    FROM
        mesh.mess
    WHERE
        mess.content::text %> _query
    ;
$$;

alter function mess_fts(text) owner to postgres;

