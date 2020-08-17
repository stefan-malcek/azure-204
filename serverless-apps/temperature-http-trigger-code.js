module.exports = async function (context, req) {
   context.log('Drive Gear Temperature Service triggered.');
 
   if(!req.body || !req.body.readings){
       context.res = {
           status: 400,
           body: "Please send an array of readings in the request body."
       }
       return;
   }
 
   for(const reading of req.body.readings){
       if(reading.temperature<=25) {
           reading.status = 'OK';
       } else if (reading.temperature<=50) {
           reading.status = 'CAUTION';
       } else {
           reading.status = 'DANGER'
       }
      
       context.log('Reading is ' + reading.status);
   }
 
   context.res = {
       // status: 200, /* Defaults to 200 */
       body:{
            "readings": req.body.readings
           }
   };
}
