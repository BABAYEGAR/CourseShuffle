<!DOCTYPE html>
<html>
  <head>
	<title>Table Sample</title>
  </head>
  <link rel="stylesheet" type="text/css" href="resources/assets/css/bootstrap.css">
  <link href="resources/assets/css/main.css" rel="stylesheet" media="screen">
 <body>
	 <div class="row">
	   <div class="col-md-12 col-sm-12 col-xs-12">
		  <div class="panel">
		      <div class="panel-heading">
		         <h4 class="panel-title">Table header</h4>
		      </div>
		      <div class="panel-body">
			      <div class="table-responsive">
				  	 <div id="dt_example" class="table-responsive example_alt_pagination clearfix">
					    <table class="table table-inverse  table-striped table-hover table-bordered pull-left" id="data-table">
					       <thead>  
							<tr>
						        <th style="width:23%">Name</th>
								<th style="width:18%">Matric Number</th>
								<th style="width:22%">Department</th>
								<th style="width:15%">Status</th>
								<th style="width:10%">Role</th>
				            </tr>
				           </thead>
				           <tbody>
				           	 <tr>
				           	 	<td>NATHAN</td>
				           	 	<td>BHU/13/04/05/0026</td>
				           	 	<td>COMPUTER SCIENCE</td>
				           	 	<td>ACTIVE</td>
				           	 	<td>USER</td>
				           	 </tr>
				           	 <tr>
				           	 	<td>KATE</td>
				           	 	<td>BHU/13/01/01/0119</td>
				           	 	<td>COMPUTER SCIENCE</td>
				           	 	<td>ACTIVE</td>
				           	 	<td>USER</td>
				           	 </tr>
				           </tbody>
					    </table>
					  </div>
					  <!-- End pagination -->    
			      </div>
			      <!-- End table-responsive -->
			  </div>
			  <!-- End panel body -->
	      </div>
	        <!-- End panel -->
	   </div>
	   <!-- End column -->
	 </div>
	   <!-- End row -->
	 <script src="resources/assets/javascript/jquery.js"></script>
	 <script src="resources/assets/javascript/jquery.datatables.js"></script>
	 <script src="resources/assets/javascript/custom-datatables.js"></script> 
 </body>
</html>