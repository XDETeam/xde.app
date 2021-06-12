CREATE OR REPLACE PROCEDURE mess.credit_approve(
 _url ltree
) AS
$$
    BEGIN
        WITH cte_data AS(
            SELECT
                xt.to,
                xmlelement(name debit, xmlattributes(_url as from), xt.content) as content
            FROM
                mess.mesh,
                XMLTABLE('//credit[@to]' PASSING content COLUMNS "to" XML PATH '@to', "content" XML PATH 'child::node()') xt
            WHERE
                url = _url
            AND
                "to" NOT LIKE '%:%'
        ) INSERT INTO mess.mesh (url, content)
        SELECT "to"::text::ltree, content FROM cte_data
        ON CONFLICT (url) DO UPDATE SET content = excluded.content;
    END
$$ language plpgsql;