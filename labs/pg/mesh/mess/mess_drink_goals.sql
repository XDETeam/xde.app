create view mess_drink_goals(at, value, sum, count) as
SELECT drink.at::date                      AS at,
       sum(drink.amount)::numeric / 2000.0 AS value,
       sum(drink.amount)                   AS sum,
       count(*)                            AS count
FROM mess.mess,
     LATERAL jsonb_to_recordset(mess.data -> 'drinks'::text) drink(at timestamp with time zone, amount integer)
WHERE mess.id = 'stan.body.water'::ltree
GROUP BY (drink.at::date)
ORDER BY (drink.at::date) DESC;

alter table mess_drink_goals
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_drink_goals to yavulan;

