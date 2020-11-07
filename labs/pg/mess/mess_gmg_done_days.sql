CREATE VIEW mesh.mess_gmg_done_days
AS
WITH cte AS (
    SELECT
		to_char(mess_gmg_done.at::timestamp with time zone, 'IYYY-MM'::text) AS month,
		to_char(mess_gmg_done.at::timestamp with time zone, 'IYYY-MM-IW'::text) AS week,
		to_char(mess_gmg_done.at::timestamp with time zone, 'ID'::text)::smallint AS day,
		mess_gmg_done.at AS date,
		sum(mess_gmg_done."time")::numeric(6, 2) / 60.00 AS hours,
		string_agg(mess_gmg_done.task, '. '::text) || '.'::text AS tasks
    FROM
		mesh.mess_gmg_done
    GROUP BY
		mess_gmg_done.at
)
SELECT cte.month,
       cte.week,
       cte.date,
       cte.day,
       cte.hours,
       sum(cte.hours) OVER (PARTITION BY cte.week ORDER BY cte.date ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS rolling_week,
       sum(cte.hours) OVER (ORDER BY cte.date ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS rolling_month,
       cte.tasks
FROM
	cte
ORDER BY
	cte.date DESC
;
