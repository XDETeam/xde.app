CREATE OR REPLACE PROCEDURE mesh.mess_drink(
	_amount integer DEFAULT 400,
	_at timestamp without time zone DEFAULT timezone('UTC'::text, now())
)
LANGUAGE sql
AS
$$
	UPDATE
        mess.mess
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
