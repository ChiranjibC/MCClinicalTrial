var clinic = {
	getPublisherDetails: function () {
		$.ajax({
		    url: "/download/details?trialName=" + $("#TrialName").val(),
		    type: "GET",
		    cache: false,
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


function download(selectedTrialKey, selectedTxId) {
    window.location.href = '/Search/Download?selectedTrialKey=' + selectedTrialKey + '&selectedTxId=' + selectedTxId;
    return false;
}

$(document).ready(function () {
    $("#TrialName").on("change", function () {
		clinic.getPublisherDetails();
    });
    clinic.getPublisherDetails();

});