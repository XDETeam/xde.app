--TODO: Probably won't be part of the deployment script
INSERT INTO mesh.node (
    path,
	content
)
VALUES
(
'//xde.team/default.xsl',
'<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:output omit-xml-declaration="yes" />

	<xsl:template match="/">
        <section>
            <h2>Meta links</h2>

            <xsl:for-each select="//link">
                <p>
                    <xsl:text>@rel: </xsl:text>
                    <xsl:value-of select="@rel"/>
				</p>
				
				<p>
                    <xsl:text> @href: </xsl:text>
                    <xsl:value-of select="@href"/>
				</p>

				<p>
                    <xsl:text> @type: </xsl:text>
                    <xsl:value-of select="@type"/>
				</p>

				<p>
                    <xsl:text> @media: </xsl:text>
                    <xsl:value-of select="@media"/>
                </p>
            </xsl:for-each>
        </section>
	</xsl:template>
</xsl:stylesheet>'
),

(
'//xde.team/labs/db-versioning',
'<node>
    <link rel="mesh:xslt" href="//xde.team/default.xsl" type="text/xsl" media="screen"/>
</node>'
)
;

--TODO: Temporary copy from the internal mess table with some drafts
INSERT INTO mesh.node (
    path,
    content
) SELECT
    url AS path,
    content
FROM
    mesh.mess
WHERE
    url IS NOT NULL
;
