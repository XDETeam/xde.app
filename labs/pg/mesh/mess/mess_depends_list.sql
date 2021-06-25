create view mess_depends_list(id, content, count) as
SELECT depends."on"                     AS id,
       (SELECT mess_1.content
        FROM mess.mess mess_1
        WHERE mess_1.id = depends."on") AS content,
       count(*)                         AS count
FROM mess.mess,
     LATERAL XMLTABLE(('//depends'::text) PASSING (mess.content)
                      COLUMNS "on" ltree PATH ('@on'::text), title text PATH ('../title'::text)) depends
GROUP BY depends."on"
ORDER BY (count(*)) DESC;

alter table mess_depends_list
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_depends_list to yavulan;
