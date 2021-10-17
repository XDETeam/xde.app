CREATE OR REPLACE FUNCTION mesh.xslt_match(
    _document xml,
    _match text,
    _pattern xml
)
RETURNS xml
AS $$
DECLARE
    _style xml;
BEGIN
    _style = format(
        '<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform">'
            '<xsl:template match="@*|node()">'
                '<xsl:copy>'
                    '<xsl:apply-templates select="@*|node()"/>'
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
END $$ LANGUAGE plpgsql;

COMMENT ON FUNCTION mesh.xslt_match(_document xml, _match text, _pattern xml)
    IS 'Applies simple xsl:template (match in _match and body in _patter) to the _document'
;
