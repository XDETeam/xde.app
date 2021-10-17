CREATE OR REPLACE VIEW mesh.mess_drink_goals
AS SELECT
	drink.at::date AS at,
	SUM(drink.amount)::numeric / 2000.0 AS value,
	SUM(drink.amount) AS sum,
	COUNT(*) AS count
FROM
	mess.mess,
	LATERAL jsonb_to_recordset(mess.data -> 'drinks'::text) drink(at timestamp with time zone, amount integer)
WHERE
	mess.id = 'stan.body.water'::ltree
GROUP BY
	(drink.at::date)
ORDER BY
	(drink.at::date) DESC
;
