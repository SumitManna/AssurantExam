Dev Test for candidates.
Wiki / Assurant Developer Coding Assessment
<br />
**Instructions:**
<br />
Build a component that will meet the following requirements. You may make assumptions if any requirements are ambiguous or vague but you must state the assumptions in your submission.
<br /><br />
**Overview:**
<br />
You will be building a simple order calculator component that will provide tax and totals. The calculator will need to account for promotions, coupons, various tax rules, etc... You may assume that the database and data-access is already developed and may mock the data-access system. No UI elements will be built for this component.
<br /><br />
**Main Business Entities:**
<br />
• Order: A set of products purchased by a customer.<br />
• Product: A specific item a customer may purchase.<br />
• Coupon: A discount for a specific product valid for a specified date range.<br />
• Promotion: A business wide discount on all products valid for a specified date range.<br />

Not all entities are necessarily listed – you may need to create additional models to complete the solution.<br />

**Business Rules:**
<br />
• Tax is calculated as a simple percentage of the order total.<br />
• Products categorized as ‘Luxury Items’ are taxed at twice the normal rate in certain states<br />
• Tax is normally calculated after applying coupons and promotional discounts. However, in the following states, the tax must be calculated prior to applying the discount: FL, NM, and NV<br />
• At this time, the business needs implementation for 5 clients that will use this application. 1 from GA, 1 from FL, 1 from NY, 1 from NM, and 1 from NV.<br />
<br />
**Technical Features:**
<br />
As tax rules change frequently, your solution should be designed for maintainability.<br />
<br />
**Requirements:**
<br />
• Adhering to the business rules stated previously:<br />
o The application shall calculate the total cost of an order<br />
o The application shall calculate the pre-tax cost of an order<br />
o The application shall calculate the tax amount of an order<br />
o The application shall store the final results in a repository upon completion of calculation.<br />
<br />
**Deliverables:**
<br />
• .NET source code implementing all business rules and requirements<br />
• Unit tests (you may choose the unit testing framework)<br />
• A list of assumptions made during the implementation and a relative assessment of risk associated with those assumptions<br />
