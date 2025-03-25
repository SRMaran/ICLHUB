function getChartColorsArray(r) {
    r = $(r).attr("data-colors");
    return (r = JSON.parse(r)).map(function (r) {
        r = r.replace(" ", "");
        if (-1 == r.indexOf("--")) return r;
        r = getComputedStyle(document.documentElement).getPropertyValue(r);
        return r || void 0
    })
}
$(document).ready(function () {
    function e() {
        var r = getChartColorsArray("#sparkline1");
        $("#sparkline1").sparkline([20, 40, 30], {
            type: "pie",
            height: "100",
            resize: !0,
            sliceColors: r
        }), r = getChartColorsArray("#sparkline2"), $("#sparkline2").sparkline([5, 6, 2, 8, 9, 4, 7, 10, 11, 12, 10, 4, 7, 10], {
            type: "bar",
            height: "100",
            resize: !0,
            barSpacing:5,
            barColor: r
        }), r = getChartColorsArray("#sparkline4"), $("#sparkline4").sparkline([0, 23, 43, 35, 44, 45, 56, 37, 40, 45, 56, 7, 10], {
            type: "line",
            width: "100",
            height: "100",
            lineColor: r,
            fillColor: "transparent",
            spotColor: r,
            lineWidth: 2,
            minSpotColor: void 0,
            maxSpotColor: void 0,
            highlightSpotColor: void 0,
            highlightLineColor: void 0
        }),
    }
    var o;
    $(window).resize(function (r) {
        clearTimeout(o), o = setTimeout(e, 500)
    }), e()
});