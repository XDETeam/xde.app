--TODO:Probably won't be part of the deployment script
INSERT INTO mesh.node (
    path,
	content
)
VALUES
(
'//xde/default.xsl',
'<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:output omit-xml-declaration="yes" />

	<xsl:template match="/">
        <section>
            <h2>Meta links</h2>

            <xsl:for-each select="//link">
                <p>
                    <xsl:text>@rel: </xsl:text>
                    <xsl:value-of select="@rel"/>

                    <xsl:text> @href: </xsl:text>
                    <xsl:value-of select="@href"/>

                    <xsl:text> @type: </xsl:text>
                    <xsl:value-of select="@type"/>

                    <xsl:text> @media: </xsl:text>
                    <xsl:value-of select="@media"/>
                </p>
            </xsl:for-each>
        </section>
	</xsl:template>
</xsl:stylesheet>'
),

(
'//xde/labs/db-version',
'<node>
    <link rel="mesh:xslt" href="//xde/default.xsl" type="text/xsl" media="screen"/>
</node>'
)
;

--TODO:0
select mesh.node_xslt_transform('//xde/labs/db-version', 'screen');
