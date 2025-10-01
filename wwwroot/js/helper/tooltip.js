(function () {
    let tooltipEl;

    // Tạo CSS trực tiếp từ JS
    const style = document.createElement("style");
    style.textContent = `
        .custom-tooltip {
            position: absolute;
            display: none;
            background: rgba(0, 0, 0, 0.75);
            color: #fff;
            padding: 6px 10px;
            border-radius: 6px;
            font-size: 14px;
            max-width: 200px;
            z-index: 9999;
            pointer-events: none;
            transition: opacity 0.2s ease-in-out;
        }
    `;
    document.head.appendChild(style);

    function showTooltip(e, target) {
        const content = target.getAttribute("data-tooltip");
        if (!content) return;

        if (!tooltipEl) {
            tooltipEl = document.createElement("div");
            tooltipEl.className = "custom-tooltip";
            document.body.appendChild(tooltipEl);
        }

        tooltipEl.innerText = content;
        tooltipEl.style.display = "block";
        tooltipEl.style.left = e.pageX + 10 + "px";
        tooltipEl.style.top = e.pageY + 10 + "px";
    }

    function moveTooltip(e) {
        if (tooltipEl && tooltipEl.style.display === "block") {
            tooltipEl.style.left = e.pageX + 10 + "px";
            tooltipEl.style.top = e.pageY + 10 + "px";
        }
    }

    function hideTooltip() {
        if (tooltipEl) {
            tooltipEl.style.display = "none";
        }
    }

    document.addEventListener("mouseover", function (e) {
        const target = e.target.closest("[data-tooltip]");
        if (target) {
            showTooltip(e, target);
        }
    });

    document.addEventListener("mousemove", function (e) {
        moveTooltip(e);
    });

    document.addEventListener("mouseout", function (e) {
        const target = e.target.closest("[data-tooltip]");
        if (target) {
            hideTooltip();
        }
    });
})();