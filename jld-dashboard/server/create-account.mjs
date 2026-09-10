import { createAccount } from './store.mjs';
import { createInterface } from 'node:readline/promises';
const input=createInterface({input:process.stdin,output:process.stdout});
const email=process.argv[2] || await input.question('Employee email: ');
input.close();
if(!process.stdin.isTTY)throw new Error('Run this command in an interactive terminal to enter the password privately.');
function secret(prompt) {
 return new Promise((resolve,reject)=>{
  process.stdout.write(prompt);process.stdin.setRawMode(true);process.stdin.resume();let value='';
  function done(){process.stdin.off('data',read);process.stdin.setRawMode(false);process.stdin.pause();process.stdout.write('\n');}
  function read(chunk){for(const char of chunk.toString()){
   if(char==='\r'||char==='\n'){done();resolve(value);return;}
   if(char==='\u0003'){done();reject(new Error('Cancelled'));return;}
   if(char==='\u007f'||char==='\b')value=value.slice(0,-1);else if(char>=' ')value+=char;
  }}
  process.stdin.on('data',read);
 });
}
const password=await secret('Password (12+ characters, hidden): ');
if(password!==await secret('Confirm password: '))throw new Error('Passwords do not match.');
await createAccount(email,password);
console.log('Employee account saved.');
