

const SequentialCalls = async (jsonModel, dateArray) => {

    for (const date of dateArray) {
        jsonModel.startDateTime = date;

        await getPost(jsonModel).then(function(result) {
                $("#payinfos").append(
                    `<tr><td>${date}</td><td>${result.totalHours}</td><td>${result.hourlyRate}</td><td>${
                    result.totalPayBeforeTaxes}</td><td>${result.federalTaxRate * 100}</td><td>${
                    result.provincialTaxRate * 100}</td><td>${result.totalFederalTaxes}</td><td>${
                    result.totalProvincialTaxes}</td><td>${result.totalPayBeforeTaxes -
                    result.totalFederalTaxes -
                    result.totalProvincialTaxes}</td></tr>`
                );
            })
            .catch(err => {
                $("#payinfos").append(`<tr><td>${date}</td><td>Error: </td><td>${err.responseText}</td></tr>`);
            });
    }
};

const getPost = async (jsonModel) => {
    return await $.ajax({
        headers: {
            'Content-Type': "application/json"
        },
        type: "POST",
        url: "https://localhost:44367/Pay/",
        data: JSON.stringify(jsonModel),
        contentType: "application/json; charset=utf-8"
    });
};

const ParallelCalls = (jsonModel, dateArray) => {

    const promises = new Array(dateArray.length);
    let i = 0;
    for (const date of dateArray) {
        jsonModel.startDateTime = date;
        promises[i] = getPost(jsonModel);
        i++;
    }

    Promise.allSettled(promises)
        .then((result) => {
            result.filter(result => result.status === "fulfilled").forEach(function(element) {
                $("#payinfos").append(
                    `<tr><td>${element.value.timeSheet.startDateTime}</td><td>${element.value.totalHours}</td><td>${
                    element.value.hourlyRate}</td><td>${
                    element.value.totalPayBeforeTaxes}</td><td>${element.value.federalTaxRate * 100}</td><td>${
                    element.value.provincialTaxRate * 100}</td><td>${element.value.totalFederalTaxes}</td><td>${
                    element.value.totalProvincialTaxes}</td><td>${element.value.totalPayBeforeTaxes -
                    element.value.totalFederalTaxes -
                    element.value.totalProvincialTaxes}</td></tr>`
                );
            });
            result.filter(result => result.status === "rejected").forEach(function(element) {
                $("#payinfos").append(`<tr><td>Error: </td><td>${element.reason.responseText}</td></tr>`);
            });
        });
};

