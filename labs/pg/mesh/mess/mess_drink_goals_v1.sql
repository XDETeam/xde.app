CREATE VIEW mesh.mess_drink_goals_v1 AS
SELECT
	drink.at::date AS at,
	sum(drink.amount)::numeric / 2000.0 AS value,
	sum(drink.amount) AS sum,
	count(*) AS count
FROM
	mess.mess,
    LATERAL XMLTABLE(('//drink'::text)
		PASSING (mess.content)
		COLUMNS amount integer PATH ('@amount'::text), at timestamp without time zone PATH ('@at'::text)) drink
GROUP BY
	drink.at::date
ORDER BY
	drink.at::date DESC
;
