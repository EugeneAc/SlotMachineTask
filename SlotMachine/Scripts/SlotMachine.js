var playCredits = 1;
var winSymbols = [];

$("#plusOneBet").click(function () {
    if (playCredits < 3) {
        playCredits++;
        $("#bet").html(playCredits);
    }
});

$("#maxBet").click(function () {
    playCredits = 3;
    $("#bet").html(playCredits);
});

$(document).ready(function () {
    var slots = $('.slot');
    slots.each(function (index) {
        $('#slot' + index).jSlots({
            number: 1,
            spinner: '#spin',
            onStart: function () {
                DisableAllActions();
            },
            onEnd: function (finalNumber) {
                 console.log($('#slot' + index + '> li')[finalNumber[0]-1].dataset.symbol);
                winSymbols.push($('#slot' + index + '> li')[finalNumber[0] - 1].dataset.symbol);
                if (winSymbols.length === $('.slot').length) {
                    $.ajax(
                        {
                            type: "POST",
                            data: { 'combination': winSymbols, 'credits': playCredits },
                            url: "/Home/CheckCombination",
                            success: function (returnValue) {
                                alert("You win " + returnValue);
                                SubstractFromTotalCredits(returnValue*-1);
                            }
                        });
                    EnableAllActions();
                    winSymbols = [];
                }
            },
            loops: index+1,
            time: Math.floor(Math.random() * 500) + (1000 * index + 1),
            easing: 'linear'
        });
    });
});

$("#spin").click(function () {
    SubstractFromTotalCredits(playCredits);
});

$("#pay").click(function () {
    alert("Game Over. Your credits " + $("#TotalCredits").html());
    DisableAllActions();
});

function SubstractFromTotalCredits(ammount) {
    var totalCredits = $("#TotalCredits").html() - ammount;
    $("#TotalCredits").html(totalCredits);
    console.log("totalCredits - "+totalCredits);
}

function DisableAllActions() {
    $("#plusOneBet").attr("disabled", "disabled");
    $("#maxBet").attr("disabled", "disabled");
    $("#spin").attr("disabled", "disabled");
    $("#pay").attr("disabled", "disabled");
}

function EnableAllActions() {
    $("#plusOneBet").removeAttr("disabled");
    $("#maxBet").removeAttr("disabled");
    $("#spin").removeAttr("disabled");
    $("#pay").removeAttr("disabled");
}