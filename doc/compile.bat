SET STR=doc
SET OUT=academic_time_planner

pdflatex "%STR%.tex"
bibtex "%STR%.aux"
pdflatex "%STR%.tex"
pdflatex "%STR%.tex"

DEL "%STR%.log"
DEL "%STR%.toc"
DEL "%STR%.aux"
DEL "%STR%.out"
DEL "%STR%.blg"
DEL "%STR%.bbl"
DEL "%STR%-blx.bib"

rename %STR%.pdf %OUT%.pdf
"%OUT%.pdf"