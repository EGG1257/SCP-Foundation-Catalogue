using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_Foundation_Catalogue
{
    internal partial class Program
    {
        //This partial class is just to store the boot sequence that simulates a linux system starting up
        static void BootSequence()
        {
            string date = DateTime.Now.ToString("ddd MMM dd HH:mm:ss yyyy");

            string[] lines = new string[]
            {
        $"[    0.000000] Linux version 5.4.0-48-generic (gcc version 9.3.0) #52-Ubuntu SMP",
        $"[    0.000000] Command line: BOOT_IMAGE=/vmlinuz-5.4.0-48-generic root=/dev/mapper/scpf-root ro quiet",
        $"[    0.000000] KERNEL supported cpus:",
        $"[    0.000000]   Intel GenuineIntel",
        $"[    0.000000]   AMD AuthenticAMD",
        $"[    0.000000] ACPI: RSDP 0x00000000000E0000 000024 (v02 SCPF  )",
        $"[    0.000000] ACPI: XSDT 0x000000003FFF0030 00003C (v01 SCPF  SCPFXSDT 00000001)",
        $"[    0.000000] x86/fpu: Supporting XSAVE feature 0x001: 'x87 floating point registers'",
        $"[    0.000000] x86/fpu: Supporting XSAVE feature 0x002: 'SSE registers'",
        $"[    0.000000] BIOS-provided physical RAM map:",
        $"[    0.000000] BIOS-e820: [mem 0x0000000000000000-0x000000000009fbff] usable",
        $"[    0.152743] microcode: microcode updated early to revision 0xea, date = 2021-01-05",
        $"[    0.414521] ACPI: IRQ0 used by override.",
        $"[    0.818730] PCI: Using configuration type 1 for base access",
        $"[    1.002341] clocksource: tsc-early: mask: 0xffffffffffffffff",
        $"[    1.230011] NET: Registered protocol family 2",
        $"[    1.545750] sd 2:0:0:0: [sda] 33554432 512-byte logical blocks: (17.2 GB/16.0 GiB)",
        $"[    1.545756] sd 2:0:0:0: [sda] Write Protect is off",
        $"[    1.545764] sd 2:0:0:0: [sda] Attached SCSI disk",
        $"[    1.812044] EXT4-fs (sda1): mounted filesystem with ordered data mode",
        $"[    2.001337] systemd[1]: Detected architecture x86-64.",
        $"[    2.001382] systemd[1]: Set hostname to <scpf-terminal>.",
        $"[    2.441233] systemd[1]: Starting Load Kernel Modules...",
        $"[    2.901100] systemd[1]: Starting Mounting H:\\ Secure Drive...",
        $"[    3.112443] systemd[1]: Starting SCP Foundation Encryption Service...",
        $"[    3.401821] systemd[1]: Started Remount Root and Kernel File Systems.",
        $"[    3.698120] systemd[1]: Verifying clearance credentials...",
        $"[    4.001553] systemd[1]: Decrypting SCP database partition...",
        $"[    4.512009] systemd[1]: Loading SCP catalogue entries...",
        $"[    4.901234] systemd[1]: Running integrity checks...",
        $"[    5.112043] systemd[1]: All systems nominal. Starting SCP Terminal.",
            };

            foreach (string line in lines)
            {
                //print each line at a random interval
                Random rnd = new Random();
                int pause = rnd.Next(10, 150);
                Console.WriteLine(line);
                Thread.Sleep(pause);
            }

            //wait a bit for a realistic feeling boot
            Thread.Sleep(800);
            ClearConsole();
        }
    }
}
