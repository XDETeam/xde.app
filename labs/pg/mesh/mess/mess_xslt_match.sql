create function mess_xslt_match(_document xml, _match text, _pattern xml) returns xml
    language plpgsql
as
$$
DECLARE
    _style xml;
BEGIN
    _style = format(
        '<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform">'
            '<xsl:template match="@*|node()">'
                '<xsl:copy>'
                    '<xsl:apply-templates select="@*|node()" />'
                '</xsl:copy>'
            '</xsl:template>'

            '<xsl:template match="%s">'
                '%s'
            '</xsl:template>'
        '</xsl:stylesheet>',
        _match,
        _pattern
    );

    RETURN xslt_process(_document::text, _style::text);
END
$$;

comment on function mess_xslt_match(xml, text, xml) is 'Applies simple xsl:template (match in _match and body in _patter) to the _document';

alter function mess_xslt_match(xml, text, xml) owner to postgres;

