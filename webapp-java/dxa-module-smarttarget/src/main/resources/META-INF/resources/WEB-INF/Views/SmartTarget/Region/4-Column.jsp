<%@ taglib prefix="dxa" uri="http://www.sdl.com/tridion-dxa" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<jsp:useBean id="region" type="com.sdl.dxa.modules.smarttarget.model.entity.smarttarget.SmartTargetRegion"
             scope="request"/>
<jsp:useBean id="markup" type="com.sdl.webapp.common.markup.Markup" scope="request"/>

<div ${markup.region(region)}>
    <c:choose>
        <c:when test="${region.withSmartTargetContent}">
            <c:forEach items="${region.entities}" var="promo">
                <dxa:entity entity="${promo}" viewName="SmartTarget:4-Column-Promotion"/>
            </c:forEach>
        </c:when>

        <c:otherwise>
            <c:set var="loopItems" value="${region.entities}" scope="request"/>
            <jsp:include page="../../Shared/Partials/4-Column-Loop.jsp"/>
        </c:otherwise>
    </c:choose>
</div>