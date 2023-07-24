This project is meant to scrape data from one or multiple PDFs containing data. For the required internal standard and analytes, the sample ID, retention time, and response are the important pieces of information.

This data will be scraped and then compiled into a CSV of the following form:

internal_standard_name, sample_id_**1**, retention_time, response

analyte_1, sample_id_**1**, retention_time, response

...

analyte_n, sample_id_**n**, retention_time, response
