$(document).ready(() => {
    $.ajax({
        type: "GET",
        url: "https://api.adviceslip.com/advice",
        dataType: "json"
    }).done((response) => {
        const text = response.slip.advice;
        const canvas = $("#canvas-tip")[0];
        canvas.width = 1600;
        const ctx = canvas.getContext('2d');
        ctx.shadowColor = "lemonchiffon";
        ctx.shadowOffsetX = 10;
        ctx.shadowOffsetY = 10
        ctx.shadowBlur = 10;
        ctx.font = "25px arial";
        const gradient = ctx.createLinearGradient(0, 0, 150, 100);
        gradient.addColorStop(0, "rgb(255, 0, 128)");
        gradient.addColorStop(1, "rgb(255, 153, 51)");
        ctx.fillStyle = gradient;
        ctx.fillText(text, 10, 50);
    });
});