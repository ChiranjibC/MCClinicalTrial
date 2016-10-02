var clinic = {
	getPublisherDetails: function () {
		$.ajax({
		    url: "/download/details?researcherName=" + $("#ResearcherName").val(),
			type: "GET",
			beforeSend: function (data)
			{
				$(".downloadHist").html("Loading....");
			},
			success: function (data)
			{
				console.log("Update Down Hist" + data);
				$(".downloadHist").html(data);
			}

		})
	}
}

$(document).ready(function () {
    $("#ResearcherName").on("change", function () {
		clinic.getPublisherDetails();
	});
});