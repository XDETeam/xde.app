create procedure mess_drink(_amount integer DEFAULT 400, _at timestamp without time zone DEFAULT timezone('UTC'::text, now()))
    language sql
as
$$
UPDATE
        mesh.mess
    SET
        data = jsonb_insert(
            data,
            '{drinks, 0}',
            jsonb_build_object('at', _at, 'amount', _amount)
        )
    WHERE
        id = 'stan.body.water'
    ;
$$;

alter procedure mess_drink(integer, timestamp) owner to postgres;

