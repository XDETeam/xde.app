create view mess_tags_list(tag, id) as
SELECT DISTINCT unnest(string_to_array(tags.tags, ','::text)) AS tag,
                mess.id
FROM mess.mess,
     LATERAL XMLTABLE(('//*[@tags]'::text) PASSING (mess.content) COLUMNS tags text PATH ('@tags'::text)) tags
ORDER BY (unnest(string_to_array(tags.tags, ','::text)));

alter table mess_tags_list
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_tags_list to yavulan;

