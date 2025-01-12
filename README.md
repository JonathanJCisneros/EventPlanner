<h1>Welcome to the Event Planner</h1>
<p>Where you can organize/manage events, invite guests, and partner up with vendors</p>
<h2>Architecture</h2>
<section>
  <h3>EventPlanner.Client</h3>
  <dl><dd><dl><dd>
    <p>
      <b style="margin-bottom: 0px;">Languages and Tools</b>
      <br />
      Vue.js Framework with TypeScript
    </p>
    <p>
      <b style="margin-bottom: 0px;">Details</b>
      <br />
      Client Layer of the application for rendering pages, client side form validations and submissions. 
      Framework will reach out to Server as needed to dynamically render content that is appropriate for 
      pages without a fresh browser reload (Header and footer remain on page).
    </p>
  </dd></dl></dd></dl>  
</section>
<hr />
<section>
  <h3>EventPlanner.API</h3>
  <dl><dd><dl><dd>
    <p>
      <b style="margin-bottom: 0px">Languages and Tools</b>
      <br />
      C#/.NET
    </p>
    <p>
      <b style="margin-bottom: 0px">Details</b>
      <br />
      API Layer of application where API calls from EventPlanner.Client are received to validate forms, return static data, dynamic content, or user authentication.
    </p>
  </dd></dl></dd></dl>
</section>
<hr />
<section>
  <h3>EventPlanner.Service</h3>
  <dl><dd><dl><dd>
    <p>
      <b style="margin-bottom: 0px">Languages and Tools</b>
      <br />
      C#/.NET
    </p>
    <p>
      <b style="margin-bottom: 0px">Details</b>
      <br />
      Service Layer of the application is responsible for managing data to/from EventPlanner.Repository and EventPlanner.Integration and returning the response to EventPlanner.API.
    </p>
  </dd></dl></dd></dl>
</section>
<section>
  <h3>EventPlanner.Repository</h3>
  <dl><dd><dl><dd>
    <p>
      <b style="margin-bottom: 0px">Languages and Tools</b>
      <br />
      C#/.NET, MySqlConnector
    </p>
    <p>
      <b style="margin-bottom: 0px">Details</b>
      <br />
      Repository Layer of the application is responsible for connecting and managing data to/from databases.
    </p>
  </dd></dl></dd></dl>
</section>
<section>
  <h3>EventPlanner.Integration</h3>
  <dl><dd><dl><dd>
    <p>
      <b style="margin-bottom: 0px">Languages and Tools</b>
      <br />
      C#/.NET
    </p>
    <p>
      <b style="margin-bottom: 0px">Details</b>
      <br />
      Integration Layer of the application is where we keep third-party integration.
    </p>
  </dd></dl></dd></dl>
</section>
<section>
  <h3>EventPlanner.Core</h3>
  <dl><dd><dl><dd>
    <p>
      <b style="margin-bottom: 0px">Languages and Tools</b>
      <br />
      C#/.NET
    </p>
    <p>
      <b style="margin-bottom: 0px">Details</b>
      <br />
      Core Layer of the application is responsible for storing core business logic, base classes, extension methods.
    </p>
  </dd></dl></dd></dl>
</section>
