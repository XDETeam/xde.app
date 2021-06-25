create view mess_drink_goals_v1(at, value, sum, count) as
SELECT drink.at::date                      AS at,
       sum(drink.amount)::numeric / 2000.0 AS value,
       sum(drink.amount)                   AS sum,
       count(*)                            AS count
FROM mess.mess,
     LATERAL XMLTABLE(('//drink'::text) PASSING (mess.content)
                      COLUMNS amount integer PATH ('@amount'::text), at timestamp without time zone PATH ('@at'::text)) drink
GROUP BY (drink.at::date)
ORDER BY (drink.at::date) DESC;

alter table mess_drink_goals_v1
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_drink_goals_v1 to yavulan;

