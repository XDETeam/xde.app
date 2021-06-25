create function id_new() returns mesh.id
    language sql
as
$$
SELECT
        (
            (EXTRACT(EPOCH FROM (NOW() - epoch)::INTERVAL)::bigint << 32)
            + (shard::bigint << 20)
            + NEXTVAL('mesh.id_sequence')
        )::mesh.id
    FROM
        env.vars
    ;
$$;

alter function id_new() owner to postgres;
