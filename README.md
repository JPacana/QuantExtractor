This project is meant to scrape data from one or multiple PDFs containing data. For the required internal standard and analytes, the sample ID, retention time, and response are the important pieces of information.

This data will be scraped and then compiled into a CSV of the following form:

Given n PDFs, each representing 1 sample...
Given m analytes...

internal_standard_name, sample_id_0, retention_time, response
... [For n samples]
internal_standard_name, sample_id_n, retention_time, response

[For m analytes]
analyte_0, sample_id_0, retention_time, response
... [For 0 to n samples]
analyte_0, sample_id_n, retention_time, response
[End m analytes loop]
